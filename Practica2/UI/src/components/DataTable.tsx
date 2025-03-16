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

const calculateHours = (entry: string, exit: string) => {
  const parseTime = (time: string) => {
    const [hours, minutes] = time.split(":" ).map(Number);
    return hours * 60 + minutes;
  };

  const totalMinutes = parseTime(exit) - parseTime(entry);
  const hours = Math.floor(totalMinutes / 60);
  const minutes = totalMinutes % 60;
  return `${hours}:${minutes < 10 ? "0" : ""}${minutes}`;
};

const DataTable: React.FC<DataTableProps> = ({ data, language }) => {
  const t = translations[language];   // Usamos el idioma seleccionado

  return (
    <div className="overflow-x-auto">
      <table className="min-w-full bg-white border border-gray-400 shadow-md rounded-lg">
        <thead>
          <tr className="text-gray-700 text-left">
            <th className="p-3 border border-gray-400 bg-gray-200">{t.date}</th>
            <th className="p-3 border border-gray-400 bg-blue-500 text-white text-center">{t.morningShift}</th>
            <th className="p-3 border border-gray-400 bg-orange-500 text-white text-center">{t.afternoonShift}</th>
            <th className="p-3 border border-gray-400 bg-green-500 text-white text-center">{t.totalMorningHours}</th>
            <th className="p-3 border border-gray-400 bg-green-500 text-white text-center">{t.totalAfternoonHours}</th>
            <th className="p-3 border border-gray-400 bg-green-700 text-white text-center">{t.totalHours}</th>
          </tr>
        </thead>
        <tbody>
          {data.map((row, index) => {
            const morningHours = calculateHours(row.morningEntry, row.morningExit);
            const afternoonHours = calculateHours(row.afternoonEntry, row.afternoonExit);
            const totalMinutes = (parseInt(morningHours.split(":" )[0]) * 60 + parseInt(morningHours.split(":" )[1])) + 
                                 (parseInt(afternoonHours.split(":" )[0]) * 60 + parseInt(afternoonHours.split(":" )[1]));
            const totalHours = `${Math.floor(totalMinutes / 60)}:${totalMinutes % 60 < 10 ? "0" : ""}${totalMinutes % 60}`;
            
            return (
              <tr key={index}>
                <td className="p-3 border border-gray-400">{row.date}</td>
                <td className="p-3 border border-gray-400">{row.morningEntry} - {row.morningExit}</td>
                <td className="p-3 border border-gray-400">{row.afternoonEntry} - {row.afternoonExit}</td>
                <td className="p-3 border border-gray-400 font-bold text-blue-700">{morningHours}</td>
                <td className="p-3 border border-gray-400 font-bold text-orange-700">{afternoonHours}</td>
                <td className="p-3 border border-gray-400 font-bold text-green-700">{totalHours}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

export default DataTable;
