namespace FSharpCards.Cards
open FSharpCards.Common.Reflection

///<summary>Card suits</summary>
type Suit = 
    | Club | Diamond | Spade | Heart
    ///<summary>Get a seq of all suits</summary>
    static member AllSuits = getAllUnionCases<Suit>()

///<summary>Card ranks.  Integer represents relative value of cards</summary>
type Rank = 
    | Two of int    | Three of int  | Four of int   | Five of int   | Six of int
    | Seven of int  | Eight of int  | Nine of int   | Ten of int    | Jack of int   
    | Queen of int  | King of int   | Ace of int
    ///<summary>Get a list of all ranks</summary>
    static member AllRanks aceHigh = 
        let ace = if aceHigh then Ace 14 else Ace 1
        [ Two 2;    Three 3;    Four 4;     Five 5;     Six 6;    Seven 7;    Eight 8;    Nine 9; 
          Ten 10;   Jack 11;    Queen 12;   King 13;    ace ]

type Card = Suit * Rank
type Hand = Card list
type Deck = Card list
type DiscardPile = Card list

[<AutoOpen>]
module Helpers =

    ///<summary>A list of all possible cards</summary>
    let allCards aceHigh :Card list = 
        aceHigh 
        |> Rank.AllRanks
        |> List.allPairs (Suit.AllSuits |> Seq.toList) 

    ///<summary>Get the suit of a card</summary>
    let getSuit card = card |> fst

    ///<summary>Get the rank of a card</summary>
    let getRank card = card |> snd