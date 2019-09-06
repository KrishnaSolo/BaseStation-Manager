#!/usr/bin/python 
import sys ,os, shutil
sys.path.append(r'\\video-01\Operations\SBETs\Apps\Python27\lib\site-packages')
sys.path.append(r'\\video-01\Operations\SBETs\Apps\Python27\lib')
import os,time ,pywinauto
from pywinauto.application import Application
from pywinauto.keyboard import send_keys, KeySequenceError
import pythoncom, time
import pyWinhook as pyHook
import win32api
import win32con 
from threading import Timer

def uMad(event):
    return False
def wegood(event):
    return True
def esc():
    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);

t = Timer(3600, esc) # Quit after 5 seconds
main_thread_id = win32api.GetCurrentThreadId();
track = 0
try:
    #os.startfile("C:\Program Files\convertToRINEX\convertToRinex.exe")
    path1 =r"\\video-01\Operations\SBETs\Apps\BaseStationManager-r2.0\base.txt"
    f =open(path1,'r')
    f1 = f.readlines()
    dest = f1[0]
    incoming= f1[1]
    f.close()
    os.startfile("\\\\video-01\\Operations\\SBETs\\Apps\\convertToRINEX\convertToRinex.exe")
    app = Application().connect(path=r"\\video-01\Operations\SBETs\Apps\convertToRINEX")
    time.sleep(0.1)
    app2 = Application(backend='uia')
    time.sleep(0.1)
    while(app.ConverttoRINEX.exists() == False):
        time.sleep(1)
    time.sleep(0.1)
    app.ConverttoRINEX.move_window(x=200,y=200)

    path = "\\\\video-01\\Operations\\SBETs\\Apps\\BaseStationManager-r2.0\\station.txt"
    with open(path,'w') as fil1:
        #fil1.write("LOG: CHECK \n")
        #fil1.write(dest)
        #fil1.write("\n")
        #part1- ensure correct settings
        def first():
            try:
                pywinauto.mouse.move(coords =(255,233))
                time.sleep(0.1)
                pywinauto.mouse.press(button='left', coords=(255,233))  
                time.sleep(0.1)
                pywinauto.mouse.release(button='left', coords=(255,233)) 
                time.sleep(0.1)
                pywinauto.mouse.move(coords =(255,255))
                time.sleep(0.1)
                pywinauto.mouse.press(button='left', coords=(255,255)) 
                time.sleep(0.1)
                pywinauto.mouse.release(button='left', coords=(255,255)) 
                win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
            except:
                #track +=1
                win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
        t = Timer(0.1, first) # Quit after 5 seconds
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

        app2 = Application(backend='uia')
        appconnect2 = app2.connect(title='Convert to RINEX')
        app2_dlg = appconnect2.Dialog


        if app.window(title = "Options").exists():
            #app2_dlg.Options.print_control_identifiers()
            #time.sleep(1)
            def second():
                try:
                    if (app2_dlg.CheckBox.get_toggle_state() == 0):
                        app2_dlg.CheckBox.click()
                        time.sleep(0.1)
                    if (app2_dlg.CheckBox2.get_toggle_state() == 0):
                        app2_dlg.CheckBox2.click()
                        time.sleep(0.1)

                    if (app2_dlg.CheckBox9.get_toggle_state() == 0):
                        app2_dlg.CheckBox9.click()
                        time.sleep(0.1)

                    if (app2_dlg.CheckBox10.get_toggle_state() == 0):
                        app2_dlg.CheckBox10.click()
                        time.sleep(0.1)
                    app.Options.move_window(x=200,y=200)
                    pywinauto.mouse.move(coords =(314,633))
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
                except:
                    #track +=1
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
            
            def sec():
                try:
                    pywinauto.mouse.press(button='left', coords=(314,633))  
                    time.sleep(0.1)
                    pywinauto.mouse.release(button='left', coords=(314,633))
                    send_keys("^a")
                    send_keys("{BACKSPACE}")
                    send_keys("C:{\}Users{\}ksolanki{\}Documents{\}rre")#"{\}{\}video-01{\}Operations{\}SBETs{\}LA19_LSU_Rinex_Files{\}%s" % dest)
                    time.sleep(0.1)
                    app.Options.Ok.close_click()
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
                except:
                    #track +=1
                    print (sys.exc_info())
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
            t = Timer(0.1, second) # Quit after 5 seconds
            main_thread_id = win32api.GetCurrentThreadId();
            t.start()
            hm = pyHook.HookManager()
            hm.MouseAll = uMad
            hm.KeyAll = uMad
            hm.MouseLeftDown = wegood
            hm.MouseLeftUp = wegood
            hm.HookMouse()
            hm.HookKeyboard()
            pythoncom.PumpMessages()
            t = Timer(0.1, sec) # Quit after 5 seconds
            main_thread_id = win32api.GetCurrentThreadId();
            t.start()
            hm = pyHook.HookManager()
            hm.MouseAll = uMad
            hm.KeyAll = wegood
            hm.MouseLeftDown = wegood
            hm.MouseLeftUp = wegood
            hm.HookMouse()
            hm.HookKeyboard()
            pythoncom.PumpMessages()

        #part2- open files
        def third():
            try:
                pywinauto.mouse.move(coords =(214,233))
                time.sleep(0.1)
                pywinauto.mouse.press(button='left', coords=(214,233))  
                time.sleep(0.1)
                pywinauto.mouse.release(button='left', coords=(214,233)) 
                time.sleep(0.1)
                pywinauto.mouse.move(coords =(223,255))
                time.sleep(0.1)
                pywinauto.mouse.press(button='left', coords=(223,255))  
                time.sleep(0.1)
                pywinauto.mouse.release(button='left', coords=(223,255)) 
                win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
            except:
                #track +=1
                win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
        t = Timer(0.1, third) # Quit after 5 seconds
        main_thread_id = win32api.GetCurrentThreadId();
        t.start()
        hm = pyHook.HookManager()
        hm.MouseAll = uMad
        hm.KeyAll = uMad
        hm.MouseLeftDown = wegood
        hm.MouseLeftUp = wegood
        hm.HookMouse()
        hm.HookKeyboard()
        pythoncom.PumpMessages()
        app23 = Application(backend='uia')
        appconnect23 = app23.connect(title='Convert to RINEX')
        app23_dlg = appconnect23.Dialog
    
        if app.window(title = "Select file(s) to convert").exists():
            #app.window(title = "Select file(s) to convert").print_control_identifiers()
            #app2_dlg.print_control_identifiers()
            def fourth():
                try:
                    app.Dialog.move_window(x=200,y=200)
                    pywinauto.mouse.move(coords =(753,250))
                    time.sleep(0.1)
                    pywinauto.mouse.press(button='left', coords=(753,250))  
                    time.sleep(0.1)
                    pywinauto.mouse.release(button='left', coords=(753,250) )
                    time.sleep(0.1)
                    send_keys("{BACKSPACE}")
                    send_keys(incoming)
                    send_keys("{ENTER}")
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
                except:
                    #track +=1
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
            def fie():
                try:
                    time.sleep(0.1)
                    pywinauto.mouse.move(coords =(623,350))
                    time.sleep(0.1)
                    pywinauto.mouse.press(button='left', coords=(623,350))  
                    time.sleep(0.1)
                    pywinauto.mouse.release(button='left', coords=(623,350) )
                    time.sleep(0.1)
                    send_keys("^a")
                    app.Dialog.Open.close_click()
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
                except:
                    #track +=1
                    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
            t = Timer(0.1, fourth) # Quit after 5 seconds
            main_thread_id = win32api.GetCurrentThreadId();
            t.start()
            hm = pyHook.HookManager()
            hm.MouseAll = uMad
            hm.KeyAll = wegood
            hm.MouseLeftDown = wegood
            hm.MouseLeftUp = wegood
            hm.HookMouse()
            hm.HookKeyboard()
            pythoncom.PumpMessages()
            t = Timer(0.1, fie) # Quit after 5 seconds
            main_thread_id = win32api.GetCurrentThreadId();
            t.start()
            hm = pyHook.HookManager()
            hm.MouseAll = uMad
            hm.KeyAll = uMad
            hm.MouseLeftDown = wegood
            hm.MouseLeftUp = wegood
            hm.HookMouse()
            hm.HookKeyboard()
            pythoncom.PumpMessages()
            time.sleep(0.1)
            n = app.cpu_usage(interval=30)
            print("CPU usage: " + str(n))

        
        appconnect2 = app2.connect(title='Convert to RINEX')
        appconnect2.ConverttoRINEX.set_focus()
        appconnect2.ConverttoRINEX.Button.wait(wait_for = "visible enabled ready exists",timeout=10000,retry_interval=5)
        appconnect2.ConverttoRINEX.set_focus()
        appconnect2.ConverttoRINEX.Button.click_input()
        print ("Clicked Convert")
        time.sleep(3)
        #def fifth():
         #       try:
          #          print ("hmm")
           #         appconnect2.ConverttoRINEX.move_window(x=200,y=200)
            #        appconnect2.ConverttoRINEX.Button.click()
             #       time.sleep(0.1)
              #      win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
               # except:
                #    win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
        #t = Timer(0.1, fifth) # Quit after 5 seconds
        #main_thread_id = win32api.GetCurrentThreadId();
        #t.start()
        #hm = pyHook.HookManager()
        #hm.MouseAll = uMad
        #hm.KeyAll = uMad
        #hm.MouseLeftDown = wegood
        #hm.MouseLeftUp = wegood
        #hm.HookMouse()
        #hm.HookKeyboard()
        #pythoncom.PumpMessages()
        #appconnect2.Button.click()
        #time.sleep(2)
        #appconnect2.ConverttoRINEX.Button.wait_not(wait_for_not = "visible enabled ready exists",timeout=10000,retry_interval=30)
        n = app.cpu_usage(interval=5)
        print ("CPU usage: " + str(n))
        while(n > 5):
            n = app.cpu_usage(interval=5)
            print ("CPU usage: " + str(n))
            time.sleep(15)
           

        print("Process is complete")
        appconnect2.ConverttoRINEX.set_focus()
        time.sleep(0.1);
        appconnect2.ConverttoRINEX.Close.click_input()
        #fil1.write("Program is complete \n")
        print ("Done")
        #input()
        #ime.sleep(0.1)
        
        #def sixth():
         #       try:
          #          appconnect2.ConverttoRINEX.move_window(x=200,y=200)
           #         appconect2.Exit.click_input()
            #        win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
             #   except:
              #      win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);
        #t = Timer(0.1, sixth) # Quit after 5 seconds
        #main_thread_id = win32api.GetCurrentThreadId();
        #t.start()
        #hm = pyHook.HookManager()
        #hm.MouseAll = uMad
        #hm.KeyAll = uMad
        #hm.MouseLeftDown = wegood
        #hm.MouseLeftUp = wegood
        #hm.HookMouse()
        #hm.HookKeyboard()
        #pythoncom.PumpMessages()
        fil1.write("True")
        fil1.close()

except Exception as e:
    def esc():
        win32api.PostThreadMessage(main_thread_id, win32con.WM_QUIT, 0, 0);

    t = Timer(0.1, esc) # Quit after 5 seconds
    main_thread_id = win32api.GetCurrentThreadId();
    t.start()
    hm = pyHook.HookManager()
    hm.MouseAll = wegood
    hm.KeyAll = wegood
    hm.HookMouse()
    hm.HookKeyboard()
    pythoncom.PumpMessages()
    print ("Error:")
    print (e.message);
      