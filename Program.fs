open Eto.Forms

[<EntryPoint; System.STAThread>]
let main argv = 
    try
        (new Application()).Run(new UI.Window())
        0
    with
        ex ->
            match MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxType.Error) with
                | DialogResult.Ok -> Application.Instance.Restart; 0
                | _ -> Application.Instance.Quit(); -1

(*[<EntryPoint>]
let main args = 
    try
        if args.Length <> 3 then
            raise (Exception("Wrong arguments.\nPlease use: [...].exe encrypt/decrypt dir password"))
        else
            if args[0].ToLower().Equals("encrypt") then
                let directory = Directory.GetFiles(args[1])
                for file in directory do
                    EncryptionBaseClass.Encrypt(file, args[2])
            elif args[0].ToLower().Equals("decrypt") then
                let directory = Directory.GetFiles(args[1])
                for file in directory do
                    if file.EndsWith(".paul") then
                        EncryptionBaseClass.Decrypt(file, args[2])
                   
    with
        | ex -> Console.Error.WriteLine(String.Format("\nERROR: {0}\n", ex)); ()

    0
*)