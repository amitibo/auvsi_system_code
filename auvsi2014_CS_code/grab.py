'''
Created on Nov 25, 2013

@author: elius
'''
import urllib
import urllib2
import os
import sqlite3
import time
from xml.dom import minidom
import socket


local_image_dir = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data\images\\"
local_crops_dir = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data\crops\\"
local_xml_dir   = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data\xml\\"
local_db_dir    = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data"

#local_image_dir = "C:\\AUVSI\\images\\"
#local_crops_dir = "C:\\AUVSI\\crops\\"
#local_xml_dir   = "C:\\AUVSI\\xml\\"
#local_db_dir    = "C:\\AUVSI\\"

addr            = "http://192.168.1.12:8070/"  # ODROID web address
loop_delay      = 0.1 # Time to wait after a failed request

con = sqlite3.connect(local_db_dir + "auvsi.db")
cur = con.cursor()
#"CREATE TABLE xmls (id INTEGER PRIMARY KEY, status TEXT, src TEXT)"

def grab(action = ""):
    '''
    Grabs an xml file from the ODROID over HTTP.
    Parse & modify the relevant parts.
    Saves images, crops and the xml file to disk. 
    '''
    
  
    try:
        url = addr + "db.php"
        if len(action) > 0:
            url = url + "?action=" + action
        xmlfile = urllib2.urlopen(url)
        
        
        xmldoc = minidom.parse(xmlfile)
        image_id = xmldoc.getElementsByTagName('image')[0].attributes['id'].value
        image_src = xmldoc.getElementsByTagName('large_img')[0].attributes['src'].value
        urllib.urlretrieve(addr + image_src, local_image_dir + os.path.basename(image_src))
        xmldoc.getElementsByTagName('large_img')[0].attributes['src'].value = local_image_dir + os.path.basename(image_src)
        
        crop_list = xmldoc.getElementsByTagName('img')
        for crop in crop_list:
            urllib.urlretrieve(addr + crop.attributes['src'].value, local_crops_dir + os.path.basename(crop.attributes['src'].value))
            crop.attributes['src'].value = local_crops_dir + os.path.basename(crop.attributes['src'].value)
    
        f = open(local_xml_dir + image_id + ".xml", 'w')
        xmldoc.writexml(f)
        f.close()
        
        sql = "INSERT INTO xmls (id, status, src) VALUES({},'false', '{}')".format(image_id, local_xml_dir + image_id + ".xml")
        cur.execute(sql)
        print time.strftime("%Y-%m-%d %H:%M:%S"),"Received image ID:",image_id
        con.commit()
    
    except IndexError:
        time.sleep(loop_delay)
    except (urllib2.URLError, urllib2.HTTPError):
        print time.strftime("%Y-%m-%d %H:%M:%S", time.gmtime()),"urllib2 error - connection working?"
        time.sleep(loop_delay)
    except socket.error:
        print time.strftime("%Y-%m-%d %H:%M:%S", time.gmtime()),"Socket error - connection working?"
        time.sleep(loop_delay)
    except Exception as e:
        print time.strftime("%Y-%m-%d %H:%M:%S", time.gmtime()),e
        time.sleep(loop_delay) # Wait for 100ms
        
    
if __name__ == '__main__':
    try:
        while True:
            grab()
    except (KeyboardInterrupt, SystemExit):
            print "got keyboard int"
            cur.close()
            db.close()
            time.sleep(5)
    except Exception as e:
        print e
        time.sleep(5)
        raise