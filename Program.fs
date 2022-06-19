open Eto.Forms

[<EntryPoint; System.STAThread>]
let main argv = 
    try
        (new Application()).Run(new UI.Window()); 0
    with
        ex ->
            match MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxType.Error) with
                | DialogResult.Ok -> Application.Instance.Restart(); 0
                | _ -> Application.Instance.Quit(); -1