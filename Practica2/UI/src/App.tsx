import { useState } from "react";
import "./App.css";
import DataTable from "./components/DataTable";
import SearchComponent from "./components/SearchComponent";
import LanguageSelector from "./components/LanguageSelector";

export interface Employee {
  id: number;
  name: string;
}

function App() {
  const [userKey, setUserKey] = useState<string>("123456");
  const [selectedEmployee, setSelectedEmployee] = useState<number | null>(1);
  const [language, setLanguage] = useState<string>("es"); // Estado para el idioma

  const employees: Employee[] = [
    { id: 1, name: "Jose" },
    { id: 2, name: "Maria" },
    { id: 3, name: "Carlos" },
  ];

  const [data, setData] = useState([
    { date: "2024-03-16", morningEntry: "08:00", morningExit: "12:00", afternoonEntry: "14:00", afternoonExit: "18:00" },
    { date: "2024-03-17", morningEntry: "08:30", morningExit: "12:30", afternoonEntry: "14:30", afternoonExit: "18:30" },
  ]);

  return (
    <>
      <div className="bg-white">
        <LanguageSelector setLanguage={setLanguage} language={language} />

        <SearchComponent
          userKey={userKey}
          setUserKey={setUserKey}
          selectedEmployee={selectedEmployee}
          setSelectedEmployee={setSelectedEmployee}
          employees={employees}
		  language={language}
        />

		<DataTable data={data} language={language} />

      </div>
    </>
  );
}

export default App;
