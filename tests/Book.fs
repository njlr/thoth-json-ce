module Thoth.Json.CE.Tests.Book

open System
open Expecto

#if FABLE_COMPILER
open Thoth.Json
open Thoth.Json.CE
#else
open Thoth.Json.Net
open Thoth.Json.Net.CE
#endif

type Author =
  {
    FirstName : string
    LastName : string
  }

type Book =
  {
    ID : Guid
    Title : string
    Year : int
    Author : Author
  }

module Decode =

  let author =
    decoder {
      let! firstName = Decode.field "firstName" Decode.string
      let! lastName = Decode.field "lastName" Decode.string

      return
        {
          FirstName = firstName
          LastName = lastName
        }
    }

  let book =
    decoder {
      let! id = Decode.field "id" Decode.guid
      let! title = Decode.field "title" Decode.string
      let! year = Decode.field "year" Decode.int
      let! author = Decode.field "author" author

      return
        {
          ID = id
          Title = title
          Year = year
          Author = author
        }
    }

let tests =
  test "Book decoder test" {
    let actual =
      """{
        "id": "81b51926-91c0-49ff-a069-53a068e9955b",
        "title": "Hyperion",
        "year": 1989,
        "author": {
          "firstName": "Dan",
          "lastName": "Simmons"
        }
      }"""
      |> Decode.fromString Decode.book

    let expected =
      Ok
        {
          ID = Guid.Parse "81b51926-91c0-49ff-a069-53a068e9955b"
          Title = "Hyperion"
          Author =
            {
              FirstName = "Dan"
              LastName = "Simmons"
            }
          Year = 1989
        }

    Expect.equal actual expected "The decoding should be correct"
  }
