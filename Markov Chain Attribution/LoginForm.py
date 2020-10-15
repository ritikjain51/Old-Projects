from flask_wtf import FlaskForm
from __init__ import login_manager
from wtforms import StringField, PasswordField, SubmitField
from wtforms.validators import DataRequired, Length
from flask_login import UserMixin

class Login(FlaskForm):

    username = StringField('Username', validators=[DataRequired(), Length(min = 2, max = 20)])
    password = PasswordField("password", validators=[DataRequired(), Length(min = 2, max = 20)])
    submit = SubmitField("Login")

class User(UserMixin):

    def __init__(self, id, active = True):
        self.id = id
        self.active = True

    def is_authenticated(self):
        return True

    def is_active(self):
        return self.active

@login_manager.user_loader
def load_user(id):
    return User(id)
