#if FABLE_COMPILER
module Thoth.Json.CE

open Thoth.Json
#else
module Thoth.Json.Net.CE

open Thoth.Json.Net
#endif

type DecoderBuilder() =
  member this.Bind(dec : Decoder<'a>, f : 'a -> Decoder<'b>) : Decoder<'b> =
    Decode.andThen f dec

  member this.Return(v : 'a) : Decoder<'a> =
    Decode.succeed v

  member this.ReturnFrom(dec : Decoder<'a>) : Decoder<'a> =
    dec

  member this.Zero() : Decoder<unit> =
    this.Return()

let decoder = DecoderBuilder()
