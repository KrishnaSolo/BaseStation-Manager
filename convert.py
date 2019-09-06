#BaseStation Convert module
#Author: Krishna Solanki
#Last Updated: 05/21/2019

#modules
import sys 
sys.path.append(r'C:\Users\ksolanki\Documents\ironpython\Lib')
sys.path.append(r'C:\Users\ksolanki\Documents\ironpython\Lib\site-packages')
import os,time,pywinauto
#app = pywinauto.application.Application()
from pywinauto.application import Application
from pywinauto.keyboard import send_keys, KeySequenceError


class Convert:
    def auto(self,dest):
        
        os.startfile("C:\Program Files\convertToRINEX\convertToRinex.exe")

        time.sleep(5)
        app = Application().connect(path=r"C:\Program Files\convertToRINEX")
        time.sleep(2)
        app2 = Application(backend='uia')
        time.sleep(1)
        app.ConverttoRINEX.draw_outline()
        time.sleep(1)
        app.ConverttoRINEX.move_window(x=200,y=200)
        time.sleep(1)

        #part1- ensure correct settings
        pywinauto.mouse.move(coords =(255,233))
        time.sleep(1)
        pywinauto.mouse.press(button='left', coords=(255,233))  
        time.sleep(1)
        pywinauto.mouse.release(button='left', coords=(255,233)) 
        time.sleep(1)
        pywinauto.mouse.move(coords =(255,255))
        time.sleep(1)
        pywinauto.mouse.press(button='left', coords=(255,255))  
        time.sleep(1)
        pywinauto.mouse.release(button='left', coords=(255,255)) 
        time.sleep(1)

        app2 = Application(backend='uia')
        time.sleep(1)
        appconnect2 = app2.connect(title='Convert to RINEX')
        time.sleep(1)
        app2_dlg = appconnect2.Dialog
        time.sleep(1)


        if app.window(title = "Options").exists():
            #app2_dlg.Options.print_control_identifiers()
            #time.sleep(1)
            if (app2_dlg.CheckBox.get_toggle_state() == 0):
                app2_dlg.CheckBox.click()
                time.sleep(1)
            if (app2_dlg.CheckBox2.get_toggle_state() == 0):
                app2_dlg.CheckBox2.click()
                time.sleep(1)

            if (app2_dlg.CheckBox9.get_toggle_state() == 0):
                app2_dlg.CheckBox9.click()
                time.sleep(1)

            if (app2_dlg.CheckBox10.get_toggle_state() == 0):
                app2_dlg.CheckBox10.click()
                time.sleep(1)
            app.Options.move_window(x=200,y=200)
            pywinauto.mouse.move(coords =(314,633))
            time.sleep(1)
            pywinauto.mouse.press(button='left', coords=(314,633))  
            time.sleep(1)
            pywinauto.mouse.release(button='left', coords=(314,633)) 
            send_keys("^a")
            send_keys("{BACKSPACE}")
            send_keys("C:{\}convert{\}"+dest)#"{\}{\}video-01{\}Operations{\}SBETs{\}LA19_LSU_Rinex_Files{\}20190521")
            time.sleep(2)
            app.Options.Ok.close_click()

        #part2- open files
        time.sleep(2)
        pywinauto.mouse.move(coords =(214,233))
        time.sleep(1)
        pywinauto.mouse.press(button='left', coords=(214,233))  
        time.sleep(1)
        pywinauto.mouse.release(button='left', coords=(214,233)) 
        time.sleep(1)
        pywinauto.mouse.move(coords =(223,255))
        time.sleep(1)
        pywinauto.mouse.press(button='left', coords=(223,255))  
        time.sleep(1)
        pywinauto.mouse.release(button='left', coords=(223,255)) 
        time.sleep(1)

        app23 = Application(backend='uia')
        time.sleep(1)
        appconnect23 = app23.connect(title='Convert to RINEX')
        time.sleep(1)
        app23_dlg = appconnect23.Dialog
        time.sleep(1)

        if app.window(title = "Select file(s) to convert").exists():
            #app.window(title = "Select file(s) to convert").print_control_identifiers()
            #app2_dlg.print_control_identifiers()
            app.Dialog.move_window(x=200,y=200)
            pywinauto.mouse.scroll(coords=(323,355),wheel_dist=4)
            time.sleep(1)
            app23_dlg.TreeItem1.click_input()
            print ("did styg")
            time.sleep(1)
            pywinauto.mouse.move(coords =(623,250))
            time.sleep(1)
            pywinauto.mouse.press(button='left', coords=(623,250))  
            time.sleep(1)
            pywinauto.mouse.release(button='left', coords=(623,250) )
            time.sleep(1)
            pywinauto.mouse.press(button='left', coords=(623,250))  
            time.sleep(1)
            pywinauto.mouse.release(button='left', coords=(623,250) )
            time.sleep(1)
            send_keys("{\}LSU_RINEX_T02{\}"+dest)
            send_keys("{ENTER}")
            pywinauto.mouse.move(coords =(623,350))
            time.sleep(1)
            pywinauto.mouse.press(button='left', coords=(623,350))  
            time.sleep(1)
            pywinauto.mouse.release(button='left', coords=(623,350) )
            time.sleep(1)
            pywinauto.mouse.move(coords =(623,350))
            time.sleep(1)
            pywinauto.mouse.press(button='left', coords=(623,350))  
            time.sleep(1)
            pywinauto.mouse.release(button='left', coords=(623,350) )
            time.sleep(1)
            #send_keys("^a")
            app.Dialog.Open.close_click()

        time.sleep(1)

        appconnect2 = app2.connect(title='Convert to RINEX')

        appconnect2.ConverttoRINEX.Button.wait(wait_for = "visible enabled ready exists",timeout=10000000,retry_interval=10)
        print ("convert ready")
        time.sleep(1)
        appconnect2.ConverttoRINEX.Button.click()
        time.sleep(1)
        appconnect2.ConverttoRINEX.Button.wait_not(wait_for_not = "visible enabled ready exists",timeout=10000000,retry_interval=10)
        print ("Program is complete")
        time.sleep(1)
        appconect2.Exit.click_input()
        res = done;
