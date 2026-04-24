> [!IMPORTANT]
> This is now built-in to Thoth!
> 
> ```fsharp
> #r "nuget: Thoth.Json.Core, 0.9.0"
> #r "nuget: Thoth.Json.System.Text.Json, 0.3.0"
> 
> open Thoth.Json.Core
> 
> open System
> 
> type Book =
>   {
>     ID : Guid
>     Title : string
>     Year : int
>   }
> 
> module Book =
> 
>   let decode =
>     decoder {
>       let! id = Decode.field "id" Decode.guid
>       let! title = Decode.field "title" Decode.string
>       let! year = Decode.field "year" Decode.int
> 
>       return
>         {
>           ID = id
>           Title = title
>           Year = year
>         }
>     }
> 
> open Thoth.Json.System.Text.Json
> 
> """
> {
>   "id": "f3394dca-1620-46a3-a850-ee2fc11f5b6c",
>   "title": "The Player of Games",
>   "year": "1988"
> }
> """
> |> Decode.fromString Book.decode
> |> printfn "%A"
> ```

# Thoth.Json.CE

Create `Thoth.Json` decoders using a Computation Expression.

```fsharp
#r "nuget: Thoth.Json.Net.CE"

open System

#if FABLE_COMPILER
open Thoth.Json
open Thoth.Json.CE
#else
open Thoth.Json.Net
open Thoth.Json.Net.CE
#endif

type Book =
  {
    ID : Guid
    Title : string
    Year : int
  }

module Book =

  let decode =
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
    
"""
{
  "id": "f3394dca-1620-46a3-a850-ee2fc11f5b6c",
  "title": "The Player of Games",
  "year": "1988"
}
"""
|> Decode.fromString Book.decode
|> printfn "%A"
```

## Install

Install from NuGet:

```bash
# Fable
paket add Thoth.Json.CE

# .NET
paket add Thoth.Json.Net.CE
```

Install as a file using [Paket](https://fsprojects.github.io/Paket/):

Add this line to your `paket.dependencies`:

```
github njlr/thoth-json-ce thoth-json-ce/Thoth.Json.CE.fs
```

Add this line to your `paket.references`:

```
File: Thoth.Json.CE.fs
```
