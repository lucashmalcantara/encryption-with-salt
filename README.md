# encryption-with-salt
Show how to encrypt a string with obfuscation using XOR + SALT technique.

# How it works?

1. We need to set a Private Key that will be used in the encryption process.
2. There is a random string called SALT (in our case only numbers), that is used to generate different string in each encryption process. We will use the SALT Positions to get parts of that string to use in the Final Key calculation process. Generaly, this value is passed through an HTTP request header called X-Salt-IV. Both server and client knows the Private Key and the SALT Positions, because those values is used in the process of encrypt/decrypt.
3. Finally, we are able to encode/decode passing as a parameter the text and the random SALT.