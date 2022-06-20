module Math

open System

let GetPercentage(value : float, max : float) : float =
    Math.Round((100.0 / (max * value)), 2)

let ByteToKiloByte(value : int) : float =
    (float)(value / 1000)