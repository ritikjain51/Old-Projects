from flask import Flask, session
from flask_login import LoginManager
import os, logging, shutil
from datetime import timedelta

## App Configuration
app =  Flask(__name__)
application = app
app.config['UPLOAD_FOLDER'] = 'FileUpload'
app.secret_key = os.urandom(20)
app.permanent_session_lifetime = timedelta(days=365)
login_manager = LoginManager(app)

## Deleting data from Upload Folder
def update_folder():
    try:
        shutil.rmtree(app.config['UPLOAD_FOLDER'])
    except FileNotFoundError as e:
        print (e)
    try:
        os.mkdir(app.config['UPLOAD_FOLDER'])
    except OSError as e:
        print(e)


import routes


##Logger Initalization
##logging.basicConfig(filename='Wiley_log.log', filemode='w', format = '%(asctime)s %(message)s')
##logger = logging.getLogger()
##logger.setLevel(logging.INFO)
##
