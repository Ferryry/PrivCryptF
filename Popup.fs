module Popup

open Eto.Forms

let Show(text : string, title : string) =
    let notify = new Notification()
    notify.Message  <- text
    notify.Title    <- title
    notify.Show()