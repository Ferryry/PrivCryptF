namespace UI

open System
open Eto.Forms
open Eto.Drawing
open Settings

type Config() as self =
    inherit Form()

    let mutable layout              = new DynamicLayout()
    //let mutable check_darkmode      = new CheckBox(Text = "Dark Mode")
    let mutable check_notify        = new CheckBox(Text = "Show Notification When Done", Size = Size(128, 32))
    let mutable button_save         = new Button(Text = "Save", Size = Size (120, 32))
    let mutable settings            = new Settings()
    let mutable textbox_key         = new TextBox(PlaceholderText = "")
    let mutable textbox_block       = new TextBox(PlaceholderText = "")

    do
        self.Title                  <- "Settings"
        self.ClientSize             <- Size (240, 128)
        self.Maximizable            <- false
        self.Resizable              <- false
        self.WindowStyle            <- WindowStyle.Utility
        self.WindowState            <- WindowState.Normal

        settings                    <- GetSettings()
        check_notify.Checked        <- settings.notify

        self.LoadLayout()

    member self.OnClickSave(e : EventArgs) =
        SaveSettings(settings)
        MessageBox.Show(self, "You need to restart this program to apply changes", "Important", MessageBoxButtons.OK, MessageBoxType.Information)
        self.Close()

    (*member self.OnChangeCheckDarkmode(e : EventArgs) =
        settings.dark   <- check_darkmode.Checked.Value*)

    member self.OnChangeCheckNotify(e : EventArgs) =
        settings.notify <- check_notify.Checked.Value

    member self.LoadLayout () =
        
        layout.BeginCentered(new Padding(), Size(240, 240), true, true)
        layout.BeginGroup("User Interface", new Padding(12, 8, 12, 8), Size(4, 4), true, false)

        check_notify.CheckedChanged.Add(self.OnChangeCheckNotify)
        //check_darkmode.CheckedChanged.Add(self.OnChangeCheckDarkmode)

        layout.Add(check_notify)
        //layout.Add(check_darkmode)

        layout.EndGroup()

        layout.EndCentered()
        layout.BeginHorizontal()

        layout.Add(null, true, true)

        layout.EndBeginHorizontal()

        layout.Add(null, true)
        button_save.Click.Add(self.OnClickSave)
        layout.Add(button_save)
        layout.EndHorizontal()

        self.Content <- layout