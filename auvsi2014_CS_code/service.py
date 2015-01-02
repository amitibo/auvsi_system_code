'''
Created on Nov 25, 2013

@author: elius
'''
from BaseHTTPServer import BaseHTTPRequestHandler,HTTPServer
import sqlite3
import glob
import os
import time

#	before running update the locations of the databases
local_image_dir = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data\images\\"
local_crops_dir = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data\crops\\"
local_xml_dir   = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data\xml\\"
local_db_dir    = r"D:\Dropbox\Dropbox\AUVSI_EE\AUVSI_2014\auvsi2014_CS_code\data"
addr            = "http://192.168.1.12:8070/"  # ODROID web address
PORT_NUMBER = 8080

   
def genxml():
    # Generate xml from the first non-sent image.
    con = sqlite3.connect(local_db_dir + "auvsi.db")
    cur = con.cursor()
    
    sql = 'SELECT * FROM xmls WHERE status = "false" ORDER BY id asc LIMIT 1'
    cur.execute(sql)
    row = cur.fetchall()
    if len(row) == 0:
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><AUVSI></AUVSI>"
    
    f = open(row[0][2], 'r')
    out = f.read()
    f.close()
    
    sql = "UPDATE xmls SET status = 'sent' WHERE id = {}".format(row[0][0]) 
    cur.execute(sql)
    con.commit()
    con.close()   
    return out

def reset_entries():
    # Sets all entries to un-set status.
    con = sqlite3.connect(local_db_dir + "auvsi.db")
    cur = con.cursor()
    sql = "UPDATE xmls SET status = 'false'"
    cur.execute(sql)
    con.commit()
    con.close()
    print "Reseted entries"
    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><AUVSI reset=\"true\"></AUVSI>"

def truncateDB():
    # Removes all entries from the DB by dropping the table and re-creating it.
    con = sqlite3.connect(local_db_dir + "auvsi.db")
    cur = con.cursor()
    sql = "DROP TABLE xmls"
    try:
        cur.execute(sql)
        con.commit()
    except sqlite3.OperationalError:
        pass
    
    sql = "CREATE TABLE xmls (id INTEGER PRIMARY KEY, status TEXT, src TEXT)"
    cur.execute(sql)
    con.close()
    print "Truncated and re-created DB\n"
    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><AUVSI truncate=\"true\"></AUVSI>" 

def delAll():
    # Removes all files and truncates DB
    files = glob.glob(local_xml_dir + "*") + glob.glob(local_image_dir + "*") + glob.glob(local_crops_dir + "*")
    print type(files)
    for f in files:
        print "Removing: {}".format(f)
        os.remove(f)
    truncateDB()
    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><AUVSI delete=\"true\"></AUVSI>"  

class myHandler(BaseHTTPRequestHandler):
    
    #Handler for the GET requests
    def do_GET(self):
        self.send_response(200)
        self.send_header('Content-type','text/xml')
        self.end_headers()
        
        try:
            if self.path == "/?action=reset":
                output = reset_entries()
            elif self.path == "/?action=truncate":
                output = truncateDB()
            elif self.path == "/?action=delete":
                output = delAll()
            else:
                output = genxml()

        except sqlite3.OperationalError as e:
            print "DB Error: \n{}".format(e.message)
            if e.message == 'no such table: xmls': output = truncateDB()
        
        self.wfile.write(output)
        
        return

try:
    delAll()
    print "deleted"
    time.sleep(15)
    server = HTTPServer(('', PORT_NUMBER), myHandler)
    print 'Started httpserver on port ' , PORT_NUMBER  
    server.serve_forever()

except KeyboardInterrupt:
    print '^C received, shutting down the web server'
    server.socket.close()
    