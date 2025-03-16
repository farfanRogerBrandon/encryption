import React from "react";

interface DataRow {
    date: string;
    morningEntry: string;
    morningExit: string;
    afternoonEntry: string;
    afternoonExit: string;
}

interface DataTableProps {
    data: DataRow[];
}

const DataTable: React.FC<DataTableProps> = ({ data }) => {
    return (
        <div className="overflow-x-auto">
            <table className="min-w-full bg-white border border-gray-400 shadow-md rounded-lg">
                <thead>
                    <tr className="text-gray-700 text-left">
                        <th className="p-3 border border-gray-400 bg-gray-200">Fecha</th>
                        <th colSpan={2} className="p-3 border border-gray-400 bg-blue-500 text-white text-center">
                            Turno Mañana
                        </th>
                        <th colSpan={2} className="p-3 border border-gray-400 bg-orange-500 text-white text-center">
                            Turno Tarde
                        </th>
                    </tr>
                    <tr className="text-gray-700 text-left">
                        <th className="p-3 border border-gray-400 bg-gray-200"></th>
                        <th className="p-3 border border-gray-400 bg-blue-100">Entrada</th>
                        <th className="p-3 border border-gray-400 bg-blue-100">Salida</th>
                        <th className="p-3 border border-gray-400 bg-orange-100">Entrada</th>
                        <th className="p-3 border border-gray-400 bg-orange-100">Salida</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map((row, index) => (
                        <tr key={index} className="text-gray-800 text-center hover:bg-gray-50">
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
