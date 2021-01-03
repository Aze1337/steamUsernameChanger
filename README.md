# steamUsernameChanger
steam username changer


![alt text](https://i.imgur.com/fjDDznw.png) 

# What does it do ?
Be able to change your username with profile url every few seconds (or any specific delay)
- random string generated for username
- random user-id string generated for url id


# How does it works
basically, a chrome browser will open and redirect to steam login page. you enter your credentials and log in. once logged, you can press any key on the console. the cookies on the chrome browser will be saved and used in the http requests.
we use chrome browser to log in to simplify the process, then it's all http requests
