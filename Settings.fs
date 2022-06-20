module Settings

open System
open System.IO
open System.Text.Json
open Eto.Drawing

type Settings () =
    //member val dark: bool           = Unchecked.defaultof<bool> with get, set
    member val keysize: int         = Unchecked.defaultof<int> with get, set
    member val blocksize: int       = Unchecked.defaultof<int> with get, set
    member val notify: bool         = Unchecked.defaultof<bool> with get, set

let public SaveSettings(settingsObject : Settings) =
    File.WriteAllText("resources\\settings.json", JsonSerializer.Serialize(settingsObject))

let public GetSettings() : Settings =
    if File.Exists("resources\\settings.json") then
        JsonSerializer.Deserialize(RSC.Resource("settings.json"))
    else
        let settings        = new Settings()
        //settings.dark       <- false
        settings.blocksize  <- 128
        settings.keysize    <- 256
        settings.notify     <- true
        settings