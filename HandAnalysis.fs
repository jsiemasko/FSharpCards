module FSharpCards.Poker.HandAnalysis
open FSharpCards.Cards
open FSharpCards.Common.Collections

///<summary>Results of the hand analysis</summary>
type HandAnalysisResult = {
    Rank: Rank; 
    Cards: Card list}

///<summary>Create a hand analysis result</summary>
let createHandAnalysisResult (cards:Card list) =
    match cards with 
    | [] -> None 
    | _ -> {
        Rank = cards.[0] |> getRank;
        Cards = cards} 
        |> Some
    

///<summary>Hand type</summary>
type HandType =
    | HighCard of HandAnalysisResult
    | Pair of HandAnalysisResult
    | ThreeOfAKind of HandAnalysisResult
    | FourOfAKind of HandAnalysisResult

///<summary>Formula for determining the high card</summary>
let highCard = 
    sortDescendingTakeFirst getRank >> createHandAnalysisResult >> Option.map HighCard

///<summary>Find sets of a specific size</summary>
let findSets numberOfCards =
    let getAllSets = 
        List.groupBy getRank 
        >> List.where (fun (_,cards) -> cards.Length = numberOfCards)
    let getHighestSet = 
        let sortByRank = List.sortByDescending fst
        let collectCards = List.collect snd
        sortByRank >> List.truncate 1 >> collectCards
    getAllSets >> getHighestSet >> createHandAnalysisResult
    
///<summary>Find the highest pair</summary>
let findPair = findSets 2 >> Option.map Pair

///<summary>Find the highest three of a kind</summary>
let findThreeOfAKind = findSets 3 >> Option.map ThreeOfAKind

///<summary>Find the highest four of a kind</summary>
let findFourOfAKind = findSets 4 >> Option.map FourOfAKind

///<summary>If we have a result just return it. Otherwise run the function we have been provided.</summary>
let apply fn (hand:Hand) result =
    match result with
    | Some _ -> result
    | None -> hand |> fn

///<summary>Analyze a hand and return an HandAnalysisResult</summary>
let analyzeHand (hand:Hand) =
    hand 
    |> findFourOfAKind
    |> apply findThreeOfAKind hand
    |> apply findPair hand
    |> apply highCard hand
