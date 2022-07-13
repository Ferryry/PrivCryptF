namespace UI

open System
open System.IO
open Eto.Forms
open Eto.Drawing
open Encryption
open RSC
open Settings

type Window() as self =
    inherit Form()

    let darkmodeForecolor           = Color.FromArgb(255, 255, 255)
    let darkmodeBackcolor           = Color.FromArgb(18, 18, 18)
    let mutable files               = Unchecked.defaultof<List<string>>
    let mutable layout              = new DynamicLayout()
    let mutable img                 = new ImageView()
    let mutable textbox             = new TextBox(PlaceholderText = "Password", Size = Size(260, 32))
    let mutable buttonConfig        = new Button(Image = new Bitmap(Resource("config.png")), Size = Size (38, 32))
    let mutable buttonFolder        = new Button(Text = "Select Files", Size = Size(100, 32))
    let mutable buttonEncrypt       = new Button(Text = "Encrypt", Size = Size(80, 32))
    let mutable buttonDecrypt       = new Button(Text = "Decrypt", Size = Size(80, 32))
    let mutable label               = new Label()
    let mutable progressbar         = new ProgressBar(Size = Size(600, 32))

    do
        self.Title                  <- "PrivCryptF - The F# En-/Decryption Tool"
        self.ClientSize             <- Size (600, 240)
        self.Maximizable            <- false
        self.Resizable              <- false

        self.LoadLayout()

    member self.OnClickEncrypt(e : EventArgs) =
        let directory = files
        progressbar.MaxValue        <- directory.Length
        for file in directory do
            if not (file.EndsWith(".aesfs")) then
                label.Text          <- String.Format("Encrypting File: {0}", file)
                Encrypt(file, textbox.Text)
            progressbar.Value       <- progressbar.Value + 1

        self.Reset()
        if(GetSettings().notify) then
            Popup.Show("Files encrypted", self.Title)

    member self.OnClickDecrypt(e : EventArgs) =
        let directory = files
        progressbar.MaxValue        <- directory.Length
        for file in directory do
            if file.EndsWith(".aesfs") then
                label.Text          <- String.Format("Decrypting File: {0}", file)
                Decrypt(file, textbox.Text)
            progressbar.Value       <- progressbar.Value + 1

        self.Reset()
        if(GetSettings().notify) then
            Popup.Show("Files decrypted", self.Title)

    member self.OnClickOpenFiles(e : EventArgs) =
        let mutable ofd = new OpenFileDialog()
        ofd.Title                       <- "Select Files"
        ofd.MultiSelect                 <- true
        ofd.CheckFileExists             <- true
        if ofd.ShowDialog(self).Equals(DialogResult.Ok) then
            for item in ofd.Filenames do
                files                   <- item :: files
                buttonEncrypt.Enabled   <- true
                buttonDecrypt.Enabled   <- true


    member self.OnClickOpenSettings(e : EventArgs) =
        let config = new Config()
        config.Show()

    member self.Reset() =
        buttonEncrypt.Enabled       <- false
        buttonDecrypt.Enabled       <- false
        progressbar.Value           <- 0
        progressbar.MaxValue        <- progressbar.Value
        label.Text                  <- null
        files                       <- Unchecked.defaultof<List<string>>

    member self.SetupDarkMode() : unit =
        self.BackgroundColor            <- darkmodeBackcolor
        progressbar.BackgroundColor     <- darkmodeBackcolor
        progressbar.BackgroundColor.Invert()
        buttonConfig.BackgroundColor    <- darkmodeBackcolor
        buttonConfig.TextColor          <- darkmodeForecolor
        buttonDecrypt.BackgroundColor   <- darkmodeBackcolor
        buttonDecrypt.TextColor         <- darkmodeForecolor
        buttonEncrypt.BackgroundColor   <- darkmodeBackcolor
        buttonEncrypt.TextColor         <- darkmodeForecolor
        buttonFolder.BackgroundColor    <- darkmodeBackcolor
        buttonFolder.TextColor          <- darkmodeForecolor
        textbox.BackgroundColor         <- darkmodeBackcolor
        textbox.TextColor               <- darkmodeForecolor

    member self.LoadLayout () : unit =

        layout.BeginCentered()

        img.Image <- new Bitmap(Resource("logo.png"))
        layout.Add(img)

        layout.EndCentered()

        layout.BeginCentered()
        layout.BeginHorizontal()
        buttonEncrypt.Enabled      <- false
        buttonEncrypt.Click.Add(self.OnClickEncrypt)
        buttonDecrypt.Enabled      <- false
        buttonConfig.Click.Add(self.OnClickOpenSettings)
        buttonDecrypt.Click.Add(self.OnClickDecrypt)
        buttonFolder.Click.Add(self.OnClickOpenFiles)
        layout.Add(buttonConfig, false)
        layout.Add(buttonFolder, false)
        layout.Add(textbox, false)
        layout.Add(buttonEncrypt, false)
        layout.Add(buttonDecrypt, false)

        layout.EndHorizontal()
        layout.EndCentered()

        layout.BeginCentered()
        layout.Add(label, false)
        layout.EndCentered()
        layout.EndVertical()

        layout.BeginCentered()
        layout.Add(null, true)
        layout.EndCentered()
        layout.EndVertical()

        layout.BeginCentered()
        layout.Add(progressbar, false)
        layout.EndCentered()
        layout.EndVertical()

        (*if(GetSettings().darkmode) then
            self.SetupDarkMode()*)

        self.Content <- layout