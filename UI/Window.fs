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

    let mutable dir                 = Unchecked.defaultof<string>
    let mutable layout              = new DynamicLayout()
    let mutable img                 = new ImageView()
    let mutable textbox             = new TextBox(PlaceholderText = "Password", Size = Size(260, 32))
    let mutable buttonConfig        = new Button(Image = new Bitmap(Resource("config.png")), Size = Size (38, 32))
    let mutable buttonFolder        = new Button(Text = "Select Folder", Size = Size(100, 32))
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
        let directory = Directory.GetFiles(dir)
        progressbar.MaxValue        <- directory.Length
        for file in directory do
            if not (file.EndsWith(".aesfs")) then
                label.Text          <- String.Format("Encrypting File: {0}", file)
                EncryptionBaseClass.Encrypt(file, textbox.Text)
            progressbar.Value       <- progressbar.Value + 1

        self.Reset()
        if(GetSettings().notify) then
            Popup.Show("Files encrypted", self.Title)

    member self.OnClickDecrypt(e : EventArgs) =
        let directory = Directory.GetFiles(dir)
        progressbar.MaxValue        <- directory.Length
        for file in directory do
            if file.EndsWith(".aesfs") then
                label.Text          <- String.Format("Decrypting File: {0}", file)
                EncryptionBaseClass.Decrypt(file, textbox.Text)
            progressbar.Value       <- progressbar.Value + 1

        self.Reset()
        if(GetSettings().notify) then
            Popup.Show("Files decrypted", self.Title)

    member self.OnClickSelectFolder(e : EventArgs) =
        let mutable sfd = new Eto.Forms.SelectFolderDialog()
        sfd.Title                   <- "Select folder"
        if sfd.ShowDialog(self).Equals(DialogResult.Ok) then
            label.Text              <- String.Format("Selected Folder: {0}", sfd.Directory)
            dir                     <- sfd.Directory
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
        dir                         <- null

    member self.LoadLayout () =

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
        buttonFolder.Click.Add(self.OnClickSelectFolder)
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

        self.Content <- layout