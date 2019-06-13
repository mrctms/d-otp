# What Is It

This is my implementation of the famous OTP (One - Time Pad) cryptography technique that cannot be cracked (if you respect all the rules).


Download <a href="https://github.com/marcktomack/d-otp/releases">here</a> and extract the folder.


# Install on Linux
```
cd d-otp
chmod +x install.sh
sudo ./install.sh
```

# How Use It

After installation:
```
d-otp

d-otp 1.0.0
Copyright (C) 2019 d-otp

  -e, --encrypt    Phrase to encrypt

  -k, --key        The secret key

  -d, --decrypt    The encrypted phrase

  --ck             Create a secret key

  --cd             Create a random dictionary

  --help           Display this help screen.

  --version        Display version information.
```
After you have created the dictionary:

Create the secret key:
```
d-otp --ck 24
```
To encrypt:
```
d-otp -e "secret phrase to encrypt" -k *secret key*
```
To decrypt:
```
d-otp -d 669719900765467828917617398992938126716362837448 -k *secret key*
```


# Uninstall on Linux

```
cd d-otp
chmod +x uninstall.sh
sudo ./uninstall.sh
```


<b>BE CAREFUL!</b>
The dict_otp.json has a fundamental role, you can choose to create a randomly dictionary or use a custom personal one.
If dict_otp.json it is present in the directory, if you choose to create a randomly dictionary, the entire contents will be deleted and replaced with a new dictionary.

Currently they are supported only letters from a to z in lowercase and space tab.

# Rules Of Use (so that the technique becomes unbreakable)
1)  The key must be long as the word you want to encrypt
2) One key one word. The key must be destroyed once used one time
3) The dictionary must be secret as the key
