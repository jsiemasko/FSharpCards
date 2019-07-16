namespace FSharpCards.Common

module Random =
    open System 

    ///<summary>Return a randomly reordered version of the input sequence</summary>
    let shuffler (r: Random) xs = 
        let nextRandom _ = r.Next()
        xs |> Seq.sortBy nextRandom

module Reflection =
    open Microsoft.FSharp.Reflection

    /// <summary>
    /// Return all values of a discriminated union as a sequence.
    /// Original source: http://fssnip.net/7VM/title/Getting-a-sequence-of-all-union-cases-in-discriminated-union
    /// </summary>
    let getAllUnionCases<'T>() =
        FSharpType.GetUnionCases(typeof<'T>)
        |> Seq.map (fun x -> FSharpValue.MakeUnion(x, Array.zeroCreate(x.GetFields().Length)) :?> 'T)

module Collections =
    ///<summary>Sort by descending using the supplied function and take the first result</summary>
    let sortDescendingTakeFirst fn =
        List.sortByDescending fn >> List.truncate 1
    