import { useState, useEffect } from 'react';
import EmployeeService from '../../services/EmployeeService';

const Home = (props: any) => {
    const id = '78190b40-d9cf-4b09-bc50-08dcad679a5c';
    const [employee, setEmployee] = useState<any>(null);
    const [employees, setEmployees] = useState<any[]>([]);

    useEffect(() => {
        const fetchEmployee = async () => {
            try {
                const fetchedEmployeeData = await EmployeeService.getEmployeeById(id);
                setEmployee(fetchedEmployeeData);
            } catch (error) {
                console.error("Error fetching employee data: ", error);
            }
        };

        const fetchAllEmployees = async () => {
            try {
                const fetchedEmployeesData = await EmployeeService.getAllEmployee();
                setEmployees(fetchedEmployeesData);
            } catch (error) {
                console.error("Error fetching all employees data: ", error);
            }
        };

        fetchEmployee();
        fetchAllEmployees();
    }, [id]);

    return (
        <div className="container">
            <div className="row">
                <div className="col-12">
                    <div className="p-3 my-3 bg-primary text-white">
                        <h1 className="mt-4">{employee ? employee.name : 'Loading...'}</h1>
                    </div>
                </div>
                <div className="col-md-6">
                    <h2>İzindeki Çalışanlar</h2>
                    <p>Açıklama:</p>
                    <table className="table table-hover">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Day of Birth</th>
                                <th>Employee Role</th>
                            </tr>
                        </thead>
                        <tbody>
                            {employees.map((emp) => (
                                <tr key={emp.id}>
                                    <td>{emp.name}</td>
                                    <td>{new Date(emp.dayOfBirth).toLocaleDateString()}</td>
                                    <td>{emp.employeeRole === 2 ? "Employee" : emp.employeeRole === 1 ? "Manager" : emp.employeeRole === 0 ? "Admin" : "Unknown"}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <div className="col-md-6">
                    <h2>Yaklaşan Tatiller</h2>
                    <p>Açıklama:</p>
                    <table className="table table-hover">
                        <thead>
                            <tr>
                                <th>Tatiller</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Data 1</td>
                            </tr>
                            <tr>
                                <td>Data 2</td>
                            </tr>
                            <tr>
                                <td>Data 3</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default Home;
