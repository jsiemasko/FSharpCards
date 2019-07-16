namespace FSharpCards.Game
open FSharpCards.Cards

///<summary>A player</summary>
type Player = {Name:string; Hand:Hand}

///<summary>Game state</summary>
type Game = {Deck:Deck; DiscardPile:DiscardPile; Players:Player list}

[<AutoOpen>]
module Helpers =
    ///<summary>Create a player with an empty hand</summary>
    let createPlayer name = {Name = name; Hand = []}
    
    ///<summary>Create a new game</summary>
    let createGame deck players = 
        {Deck = deck; DiscardPile = []; Players = players}
    
    ///<summary>Take a card and a player and return a new player with the card in hand</summary>
    let addCardToHand card player = 
        {player with Hand = player.Hand @ [card]}
    
    ///<summary>Give a card to a player returning the new game state</summary>
    let playerDrawCard game targetPlayer =
        let card, deck = (game.Deck.Head, game.Deck.Tail)
        let addIfTargetPlayer player = 
            if player = targetPlayer then player |> addCardToHand card 
            else player
        {game with 
            Players = game.Players |> List.map addIfTargetPlayer
            Deck = deck}
    
    ///<summary>Give a card to all players</summary>
    let allPlayersDrawCard game =
        let cardsToDistribute, newDeck = game.Deck |> List.splitAt game.Players.Length
        let players = 
            game.Players 
            |> List.zip cardsToDistribute
            |> List.map (fun (card,player) -> player |> addCardToHand card)
        {game with 
            Players = players
            Deck = newDeck
        }
    
    ///<summary>Deal an initial hand of cards to each player</summary>
    let initialDeal numberOfCards game = 
        [1..numberOfCards] 
        |> List.fold (fun g _ -> g |> allPlayersDrawCard) game