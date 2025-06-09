#namespace backendAlquimia.Fastapi_accords
#{
#    public class main
#    {
        
from fastapi import FastAPI, Query
from fastapi.middleware.cors import CORSMiddleware
import pandas as pd

app = FastAPI()

# Permitir acceso desde frontend en Next.js
app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:3000"],  # Cambiar por dominio real en producción
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# Cargar CSV al iniciar
csv_path = "fra_cleaned.csv"
df = pd.read_csv(csv_path, encoding="latin1", sep=";")

@app.get("/api/perfume")
def search_perfume(name: str = Query(..., alias="q")):
    result = df[df["Perfume"].str.contains(name, case=False, na=False)]

    #print("RESULTADO FILTRADO:", result[["Perfume", "mainaccord1", "mainaccord2", "mainaccord3"]].to_dict())
    #perfume = result.iloc[0]
    if result.empty:
        return {"message": "Perfume not found", "notes": []}
    
    perfume = result.iloc[0]  # Tomamos el primero que coincida
    notes = [
        perfume.get("mainaccord1"),
        perfume.get("mainaccord2"),
        perfume.get("mainaccord3")
    ]
    return {
        "perfume": perfume["Perfume"],
        "brand": perfume["Brand"],
        "notes": [n for n in notes if pd.notna(n)]
    }

#    }
#}
