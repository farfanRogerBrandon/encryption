import { useState } from "react";
import { translations } from "./translations"; // Importa las traducciones

interface Props {
  setLanguage: (lang: string) => void;
  language: string; // Recibe el idioma actual
}

const LanguageSelector: React.FC<Props> = ({ setLanguage, language }) => {
  const [selectedLang, setSelectedLang] = useState(language);

  const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const newLang = event.target.value;
    setSelectedLang(newLang);
    setLanguage(newLang);
  };

  const t = translations[language]; // Obtiene las traducciones según el idioma seleccionado

  return (
    <div>
      <label htmlFor="language-select">{t.selectLanguage}:</label>
      <select id="language-select" value={selectedLang} onChange={handleChange}>
        <option value="es">Español</option>
        <option value="en">Inglés</option>
        <option value="ko">Coreano</option>
      </select>
    </div>
  );
};

export default LanguageSelector;
