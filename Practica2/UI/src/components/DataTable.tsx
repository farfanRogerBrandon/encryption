import React from "react";
import { translations } from "./translations";

interface DataRow {
  date: string;
  morningEntry: string;
  morningExit: string;
  afternoonEntry: string;
  afternoonExit: string;
}

interface DataTableProps {
  data: DataRow[];
  language: string; // Recibimos el idioma como prop
}

const DataTable: React.FC<DataTableProps> = ({ data, language }) => {
const t = translations[language];   // Usamos el idioma seleccionado

  return (
    <div className="overflow-x-auto">
      <table className="min-w-full bg-white border border-gray-400 shadow-md rounded-lg">
        <thead>
          <tr className="text-gray-700 text-left">
            <th className="p-3 border border-gray-400 bg-gray-200">{t.date}</th>
            <th colSpan={2} className="p-3 border border-gray-400 bg-blue-500 text-white text-center">
              {t.morningShift}
            </th>
            <th colSpan={2} className="p-3 border border-gray-400 bg-orange-500 text-white text-center">
              {t.afternoonShift}
            </th>
          </tr>
          <tr className="text-gray-700 text-left">
            <th className="p-3 border border-gray-400 bg-gray-200"></th>
            <th className="p-3 border border-gray-400 bg-blue-100">{t.entry}</th>
            <th className="p-3 border border-gray-400 bg-blue-100">{t.exit}</th>
            <th className="p-3 border border-gray-400 bg-orange-100">{t.entry}</th>
            <th className="p-3 border border-gray-400 bg-orange-100">{t.exit}</th>
          </tr>
        </thead>
        <tbody>
          {data.map((row, index) => (
            <tr key={index}>
              <td className="p-3 border border-gray-400">{row.date}</td>
              <td className="p-3 border border-gray-400">{row.morningEntry}</td>
              <td className="p-3 border border-gray-400">{row.morningExit}</td>
              <td className="p-3 border border-gray-400">{row.afternoonEntry}</td>
              <td className="p-3 border border-gray-400">{row.afternoonExit}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default DataTable;
