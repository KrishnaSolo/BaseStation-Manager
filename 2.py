import pythoncom, time
import pyWinhook as pyHook
import sys ,os,ctypes
sys.path.append(r'\\video-01\Operations\SBETs\Apps\Python27\Lib')
sys.path.append(r'\\video-01\Operations\SBETs\Apps\Python27\Lib\site-packages')
import os,time ,pywinauto
from pywinauto.application import Application
from pywinauto.keyboard import send_keys, KeySequenceError
def uMad(event):
    return False
def wegood(event):
    return True
import win32api
import win32con 
from threading import Timer

class Automate:
    def __init__(self,time,path, dest):
        self.time = time
        self.path = path
        self.dest = dest
    


def on_timer():
        pywinauto.mouse.move(coords =(365,173))
        time.sleep(1)
        pywinauto.mouse.press(button='left', coords=(365,173))  
        time.sleep(1)
        pywinauto.mouse.release(button='left', coords=(365,173)) 
        time.sleep(1)
        pywinauto.mouse.move(coords =(365,255))
        time.sleep(1)
        pywinauto.mouse.press(button='left', coords=(255,255))  
        time.sleep(1)
        pywinauto.mouse.release(button='left', coords=(255,255)) 
        time.sleep(1)
        win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);

t = Timer(2.0, on_timer) # Quit after 5 seconds
main_thread_id = win32api.GetCurrentThreadId();
t.start()
hm = pyHook.HookManager()
hm.MouseAll = uMad
hm.MouseLeftDown = wegood
hm.MouseLeftUp = wegood
hm.KeyAll = uMad
hm.HookMouse()
hm.HookKeyboard()
pythoncom.PumpMessages()
print"hre"
time.sleep(192)
pywinauto.mouse.move(coords=(321,228))
time.sleep(1)
pywinauto.mouse.move(coords=(321,528))
time.sleep(1)
pywinauto.mouse.move(coords=(121,228))
time.sleep(1)
pywinauto.mouse.move(coords=(321,28))
time.sleep(1)
t = Timer(22.0, on_timer) # Quit after 5 seconds
t.start()
hm = pyHook.HookManager()
hm.MouseAll = wegood
hm.KeyAll = wegood
hm.HookMouse()
hm.HookKeyboard()
#ctypes.windll.user32.PostQuitMessage(0)
pythoncom.PumpMessages()
print("de")