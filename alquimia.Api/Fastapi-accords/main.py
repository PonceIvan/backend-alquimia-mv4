    
from fastapi import FastAPI, Query
from fastapi.middleware.cors import CORSMiddleware
import requests
from bs4 import BeautifulSoup
import pandas as pd
from webdriver_manager.chrome import ChromeDriverManager
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.chrome.options import Options
app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:3000",
    "https://frontend-alquimia.vercel.app"],  
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

csv_path = "fra_cleaned.csv"
df = pd.read_csv(csv_path, encoding="latin1", sep=";")

accord_translations = {
    "rose": "rosa",
    "citrus": "FAMILIA CÍTRICA",
    "fruity": "FAMILIA FRUTAL",
    "aromatic": "FAMILIA AROMÁTICO",
    "white floral": "floral blanco",
    "woody": "FAMILIA AMADERADO",
    "powdery": "FAMILIA EMPOLVADO",
    "leather": "cuero",
    "green": "verde",
    "rubber": "caucho",
    "floral": "FAMILIA FLORAL",
    "ozonic": "ozónico",
    "vinyl": "vinilo",
    "musky": "FAMILIA ALMIZCLADO",
    "yellow floral": "floral amarillo",
    "earthy": "FAMILIA TERROSO",
    "warm spicy": "especiado cálido",
    "fresh spicy": "especiado fresco",
    "fresh": "fresco",
    "sweet": "dulce",
    "amber": "FAMILIA ÁMBAR",
    "vanilla": "vainilla",
    "tropical": "tropical",
    "lavender": "lavanda",
    "almond": "almendra",
    "violet": "violeta",
    "iris": "iris",
    "cherry": "cereza",
    "aquatic": "acuático",
    "aldehydic": "FAMILIA ALDEHÍDICO",
    "animalic": "animalico",
    "oud": "oud (madera de agar)",
    "marine": "FAMILIA MARINO",
    "metallic": "metálico",
    "lactonic": "lactónico",
    "coffee": "café",
    "tuberose": "nardo",
    "caramel": "caramelo",
    "smoky": "FAMILIA AHUMADO",
    "coconut": "coco",
    "soft spicy": "especiado suave",
    "patchouli": "pachulí",
    "sand": "arena",
    "anis": "anís",
    "mossy": "musgoso",
    "honey": "miel",
    "mineral": "mineral",
    "chocolate": "chocolate",
    "cacao": "cacao",
    "coca-cola": "coca-cola",
    "tobacco": "tabaco",
    "savory": "salado/sabroso",
    "plastic": "plástico",
    "herbal": "FAMILIA HERBAL",
    "nutty": "nuez/nuez moscada",
    "soapy": "jabonoso",
    "balsamic": "balsámico",
    "salty": "salado",
    "asphault": "asfalto",
    "champagne": "champán",
    "beeswax": "cera de abejas",
    "cinnamon": "canela",
    "whiskey": "whisky",
    "rum": "ron",
    "industrial glue": "pegamento industrial",
    "oriental": "oriental",
    "camphor": "FAMILIA ALCANFORADO",
    "hot iron": "hierro caliente",
    "cannabis": "cannabis",
    "vodka": "vodka",
    "clay": "arcilla",
    "wine": "vino",
    "brown scotch tape": "cinta adhesiva marrón",
    "spicy": "FAMILIA ESPECIADO",
    "gourmand": "FAMILIA GOURMAND",
    "oily": "aceitoso",
    "paper": "papel",
    "conifer": "conífero",
    "bitter": "amargo",
    "alcohol": "alcohol",
    "sour": "agrio"
}


@app.get("/api/perfume")
def search_perfume(name: str = Query(..., alias="q")):
    normalized_name = name.replace(" ", "-").lower()
    results = df[df["Perfume"].str.lower().str.contains(normalized_name, na=False)]

    if results.empty:
        return {"message": "Perfume not found", "notes": []}
    
    #perfume = results.iloc[0]  # Tomamos el primero que coincida
    #notes = [
    #    perfume.get("mainaccord1"),
    #    perfume.get("mainaccord2"),
    #    perfume.get("mainaccord3")
    #]
    perfumes_list = []
    for _, row in results.head(4).iterrows():
        notes = [
            row.get("mainaccord1"),
            row.get("mainaccord2"),
            row.get("mainaccord3")
        ]
        translated_notes = [accord_translations.get(n.lower(), n) for n in notes if pd.notna(n)]
        perfumes_list.append({
            "perfume": row["Perfume"],
            "brand": row["Brand"],
            "notes": translated_notes,
            "url": row.get("url")
        })

    return {"results": perfumes_list}
        #translated_notes = [accord_translations.get(n.lower(), n) for n in notes if pd.notna(n)]
        #return {
        #    "perfume": perfume["Perfume"],
        #    "brand": perfume["Brand"],
        #    #"notes": [n for n in notes if pd.notna(n)]
        #    "notes": translated_notes
        #}


    
@app.get("/api/perfume-image")
def get_perfume_image(url: str = Query(...)):
    chrome_options = Options()
    chrome_options.binary_location = r"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe"  
    #chrome_options.add_argument("--headless=new")
    chrome_options.add_argument("--disable-gpu")
    chrome_options.add_argument("--no-sandbox")
    chrome_options.add_argument("--disable-dev-shm-usage")

    service = Service("./chromedriver.exe")
    driver = webdriver.Chrome(service=service, options=chrome_options)

    driver.get(url)
    driver.implicitly_wait(5)

    imgs = driver.find_elements("tag name", "img")
    perfume_img = None

    for i, im in enumerate(imgs):
        try:
            src = im.get_attribute("src")
            if src and "/mdimg/perfume-thumbs/" in src:
                perfume_img = src
                break
        except Exception as e:
            print(f"Error en imagen {i}: {e}")

    driver.quit()

    return {"image": perfume_img}


