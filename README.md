#PrivCryptF-AES

Simple AES Encryption and Decryption tool written in F#.

![Screenshot 2022-06-19](https://user-images.githubusercontent.com/88947587/174459367-db0047e9-18f1-4dcc-a5d4-c3060314bbbb.jpg)

~For CMD Version:<br>
Usage: `PrivCryptF.exe encrypt/decrypt directory password`<br>
Example: `PrivCryptF.exe encrypt C:\documents\anything 123helloworld`<br>
Output: Encrypted file with `*.aesfs` file extension~

Usage: Double Click exe file<br>
Output: Encrypted file(s) with `*.aesfs` file extension

### Used Cryptography

I used [Advanced Encryption Standard (AES)](https://de.wikipedia.org/wiki/Advanced_Encryption_Standard) with a 256-bit key, 128-bit block size and Cipher Feedback Mode (CFB).

### Used Frameworks

- [ETO.Forms](https://github.com/picoe/Eto)

### To Do

- Adding a simple GUI (Windows Forms or Eto Forms) | ✔️ Already Implemented
- Adding more options like custom Cipher Mode and custom Padding Mode
- Adding compress mode to shrink the file's size
- Adding Dark Mode
