import threading
from queue import Queue
import time

lock = threading.Lock()
q = Queue()


def worker():
    time.sleep(10000)


for i in range(100000):
        t = threading.Thread(target=worker)
        t.daemon = True
        try:
            t.start()
            # print(i)
        except Exception:
            # print("Broke on {}".format(i))
            print(i - 1)
            exit(i - 1)
