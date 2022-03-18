module Thoth.Json.CE

#if FABLE_COMPILER
open Thoth.Json
#else
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
