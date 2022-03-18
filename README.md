# Thoth.Json.CE

Create `Thoth.Json` decoders using a Computation Expression.

```fsharp
#if FABLE_COMPILER
open Thoth.Json
#else
open Thoth.Json.Net
#endif

open Thoth.Json.CE

type Book =
  {
    ID : Guid
    Title : string
    Year : int
  }

module Decode =

  let book =
    decoder {
      let! id = Decode.field "id" Decode.guid
      let! title = Decode.field "title" Decode.string
      let! year = Decode.field "year" Decode.int

      return
        {
          ID = id
          Title = title
          Year = year
        }
    }
```

## Install

Install using [Paket](https://fsprojects.github.io/Paket/).

Add this line to your `paket.dependencies`:

```
github njlr/thoth-json-ce Thoth.Json.CE.fs
```

Add this line to your `paket.references`:

```
File: Thoth.Json.CE.fs
```
