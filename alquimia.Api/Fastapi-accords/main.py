    
from fastapi import FastAPI, Query
from fastapi.middleware.cors import CORSMiddleware
import pandas as pd

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:3000"],  
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

csv_path = "fra_cleaned.csv"
df = pd.read_csv(csv_path, encoding="latin1", sep=";")

@app.get("/api/perfume")
def search_perfume(name: str = Query(..., alias="q")):
    result = df[df["Perfume"].str.contains(name, case=False, na=False)]

    if result.empty:
        return {"message": "Perfume not found", "notes": []}
    
    perfume = result.iloc[0]  # Tomamos el primero que coincida
    notes = [
        perfume.get("mainaccord1"),
        perfume.get("mainaccord2"),
        perfume.get("mainaccord3")
    ]
    # Imprimir los valores únicos de las columnas mainaccord1, mainaccord2 y mainaccord3
    unique_accords = pd.concat([
        df["mainaccord1"].dropna(),
        df["mainaccord2"].dropna(),
        df["mainaccord3"].dropna()
    ]).unique().tolist()
    print("Valores únicos de los acordes:", unique_accords)
    return {
        "perfume": perfume["Perfume"],
        "brand": perfume["Brand"],
        "notes": [n for n in notes if pd.notna(n)]
    }

