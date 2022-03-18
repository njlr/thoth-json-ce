open Expecto
open Thoth.Json.CE.Tests

let tests =
  testList "CE" [
    Ast.tests
    Book.tests
  ]

[<EntryPoint>]
let main args =
  runTestsWithCLIArgs [] args tests
