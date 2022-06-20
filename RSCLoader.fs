module RSC

open System
open System.IO

let Resource(file : string) : byte[] =
    if not (File.Exists(String.Format("resources\\{0}", file))) then
        raise(new FileNotFoundException("Resource does not exists or could not be found!"))
    else
        let mutable ms = new MemoryStream()
        use fs = new FileStream(String.Format("resources\\{0}", file), FileMode.Open)
        fs.CopyTo(ms)
        ms.ToArray()