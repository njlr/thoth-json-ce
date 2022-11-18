module Thoth.Json.CE.Tests.Ast

open Expecto

#if FABLE_COMPILER
open Thoth.Json
open Thoth.Json.CE
#else
open Thoth.Json.Net
open Thoth.Json.Net.CE
#endif

[<RequireQualifiedAccess>]
type Ast =
  | Ident of string
  | Int of int
  | List of Ast list

module Decode =

  let ast : Decoder<Ast> =
    let rec loop () =
      decoder {
        let! ty = Decode.field "type" Decode.string

        match ty with
        | "ident" ->
          let! name = Decode.field "name" Decode.string

          return Ast.Ident name
        | "int" ->
          let! value = Decode.field "value" Decode.int

          return Ast.Int value
        | "list" ->
          let! elements = Decode.field "elements" (Decode.list (loop ()))

          return Ast.List elements
        | _ ->
          return! Decode.fail $"Unexpected AST node type \"{ty}\""
      }
    loop ()

let tests =
  test "AST decoder test" {
    let actual =
      """{
        "type": "list",
        "elements": [
          {
            "type": "int",
            "value": 123
          },
          {
            "type": "ident",
            "name": "x"
          },
          {
            "type": "list",
            "elements": [
              {
                "type": "ident",
                "name": "y"
              }
            ]
          }
        ]
      }"""
      |> Decode.fromString Decode.ast

    let expected =
      Ast.List [
        Ast.Int 123
        Ast.Ident "x"
        Ast.List [
          Ast.Ident "y"
        ]
      ]
      |> Ok

    Expect.equal actual expected "The decoding should be correct"
  }
