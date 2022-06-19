module RSC

open System
open System.IO

let Resource(file : string) : Stream =
    if not (File.Exists(String.Format("resources\\{0}", file))) then
        raise(new FileNotFoundException("Resource does not exists or could not be found!"))
    else
        new FileStream(String.Format("resources\\{0}", file), FileMode.Open)