﻿namespace FSharp.Data

open NUnit.Framework
open FsUnit
open FsCheck
open FsCheck.NUnit
open MsgPackValue

[<TestFixture>]
module MsgPackTest =

  open Limited

  [<Test>]
  let ``nil`` () =
    Nil
    |> MsgPack.pack
    |> MsgPack.unpack
    |> should equal (Some Nil)
  
  [<Test>]
  let ``false`` () =
    False
    |> MsgPack.pack
    |> MsgPack.unpack
    |> should equal (Some False)

  [<Test>]
  let ``true`` () =
    True
    |> MsgPack.pack
    |> MsgPack.unpack
    |> should equal (Some True)

  [<Test>]
  let ``uint8`` () =
    check <| fun x ->
      let value = UInt8 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``uint16`` () =
    check <| fun x ->
      let value = UInt16 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``uint32`` () =
    check <| fun x ->
      let value = UInt32 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``uint64`` () =
    check <| fun x ->
      let value = UInt64 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``int8`` () =
    check <| fun x ->
      let value = Int8 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``int16`` () =
    check <| fun x ->
      let value = Int16 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``int32`` () =
    check <| fun x ->
      let value = Int32 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``int64`` () =
    check <| fun x ->
      let value = Int64 x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``float32`` () =
    check <| fun x ->
      let value = Float32 x
      match value |> MsgPack.pack |> MsgPack.unpack with
      | Some (Float32 actual) when System.Single.IsNaN(actual) -> true
      | Some (Float32 actual) -> actual = x
      | _ -> false

  [<Test>]
  let ``float64`` () =
    check <| fun x ->
      let value = Float64 x
      match value |> MsgPack.pack |> MsgPack.unpack with
      | Some (Float64 actual) when System.Double.IsNaN(actual) -> true
      | Some (Float64 actual) -> actual = x
      | _ -> false

  [<Test>]
  let ``string`` () =
    check <| fun x ->
      let value = String x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``binary`` () =
    check <| fun x ->
      let value = Binary x
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``array`` () =
    check <| fun xs ->
      let value =
        xs
        |> Microsoft.FSharp.Collections.Array.map String
        |> Array
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)

  [<Test>]
  let ``map`` () =
    check <| fun (xs: (string * string) list) ->
      let value =
        xs
        |> Microsoft.FSharp.Collections.List.map (fun (k,v) -> (String k, String v))
        |> Microsoft.FSharp.Collections.Map.ofList
        |> Map
      Some value = (value |> MsgPack.pack |> MsgPack.unpack)
