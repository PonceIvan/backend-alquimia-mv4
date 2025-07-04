from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.chrome.options import Options
import time

# Configurar Brave
chrome_options = Options()
chrome_options.binary_location = r"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe"
chrome_options.add_argument("--disable-gpu")
chrome_options.add_argument("--no-sandbox")
# Puedes comentar headless si quieres ver la ventana:
# chrome_options.add_argument("--headless")

service = Service(r"./chromedriver.exe")

driver = webdriver.Chrome(service=service, options=chrome_options)

url = "https://www.fragrantica.com/perfume/xerjoff/accento-overdose-pride-edition-74630.html"

print("Abriendo página...")
driver.get(url)

# Esperar a que cargue el contenido dinámico
time.sleep(5)

print("Buscando imágenes...")
imgs = driver.find_elements("tag name", "img")

#img_urls = []
for i, im in enumerate(imgs):
    try:
        src = im.get_attribute("src")
        if src and "/mdimg/perfume-thumbs/375x500" in src:
            perfume_img = src
            break
    except Exception as e:
        print(f"Error en imagen {i}: {e}")

driver.quit()

print("Resultado final:")
print(perfume_img)
