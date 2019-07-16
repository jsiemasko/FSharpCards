
open FSharpCards.Cards
open FSharpCards.Common.Random
open FSharpCards.Game
open FSharpCards.Poker.HandAnalysis
open System

///<summary>A version of the shuffler with the random source partially applied</summary>
let shuffle = shuffler (Random())

///<summary>The initial state of the deck</summary>
let initialDeck = allCards >> shuffle >> Seq.toList

[<EntryPoint>]
let main argv = 
    
    let mutable game = 
        createGame
            (initialDeck true)
            (["Jon"; "Eric"; "Dave"; "Kirk"; "Wyatt"; "Jen"; "Lyra"] |> List.map createPlayer)  
        |> initialDeal 7
    
    game |> printfn "%A"

    game.Players |> List.map (fun p -> p.Name, p.Hand |> analyzeHand) |> printfn "%A"

    0