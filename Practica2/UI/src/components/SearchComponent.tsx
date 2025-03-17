import { Employee } from "../App";

// Definimos las interfaces para los props
interface SearchProps {
  userKey: string;
  setUserKey: React.Dispatch<React.SetStateAction<string>>;
  selectedEmployee: number | null;
  setSelectedEmployee: React.Dispatch<React.SetStateAction<number | null>>;
  employees: Employee[];
}

const SearchComponent = ({
  userKey,
  setUserKey,
  selectedEmployee,
  setSelectedEmployee,
  employees,
}: SearchProps) => {

  // Maneja el cambio del input "User Key"
  const handleUserKeyChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUserKey(event.target.value);
  };

  // Maneja el cambio del select "Empleado"
  const handleEmployeeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedEmployee(Number(event.target.value));
  };

  return (
    <div className="max-w-md mx-auto p-4 border rounded-lg shadow-md bg-white">
      <h2 className="text-xl font-semibold mb-4">Formulario de Usuario</h2>
      <form>
        {/* Input de User Key */}
        <div className="mb-4">
          <label htmlFor="userKey" className="block text-sm font-medium text-gray-700">
            User Key
          </label>
          <input
            id="userKey"
            type="text"
            value={userKey}
            onChange={handleUserKeyChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Ingresa tu User Key"
          />
        </div>

        {/* Select de Empleado */}
        <div className="mb-4">
          <label htmlFor="employee" className="block text-sm font-medium text-gray-700">
            Seleccione al Empleado
          </label>
          <select
            id="employee"
            value={selectedEmployee ?? ""}
            onChange={handleEmployeeChange}
            className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="">Seleccione...</option>
            {employees.map((employee) => (
              <option key={employee.id} value={employee.id}>
                {employee.name}
              </option>
            ))}
          </select>
        </div>
      </form>
    </div>
  );
};

export default SearchComponent;
