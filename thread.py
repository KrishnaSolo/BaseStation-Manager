import pythoncom, time
import pyWinhook as pyHook
import sys ,os,ctypes
sys.path.append(r'\\video-01\Operations\SBETs\Apps\Python27\Lib')
sys.path.append(r'\\video-01\Operations\SBETs\Apps\Python27\Lib\site-packages')
import os,time ,pywinauto
from pywinauto.application import Application
from pywinauto.keyboard import send_keys, KeySequenceError

time.sleep(1)
pywinauto.mouse.move(coords=(321,228))
time.sleep(1)
pywinauto.mouse.move(coords=(321,528))
time.sleep(1)
pywinauto.mouse.move(coords=(121,228))
time.sleep(1)
pywinauto.mouse.move(coords=(321,28))
