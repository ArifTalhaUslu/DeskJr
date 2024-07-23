import { useEffect, useState, createContext, useContext } from "react";
import employeeService from "../../services/EmployeeService";
import Card from "../CommonComponents/Card";
import Board from "../CommonComponents/Board";


const Employee: any = () => {
  const [items, setItems] = useState([]);

  useEffect(() => {
    getList();
  }, []);

  const getList = async () => {
    employeeService.getAllEmployee().then((data) => {
      setItems(data);
    });
  };

  const handleEdit = async (employee: any) => {
    employeeService.updateEmployee(employee).then((data) => {});
    //console.log("Edit item:", item);
    //kullanım örneği :
    //service.updateItem(item);
  };

  const handleDelete = async (id: any) => {
    employeeService.deleteEmployee(id).then((data) => {});
    //console.log("Delete item:", item);
    //kullanım örneği :
    //service.deleteItem(item.id);
  };

  const isEditable = (item: any) => {
    
    //kayıtlarda güncelleme yapılacak ise hangi şartı sağlaması gerektiğinin seçimidir. şart için true dönmek o durumu editable yapar
    //örneğin:
    //if (item.isBoss) return false; //bu durum için açıklama : isBoss özelliği true olan kayıtlar uneditable durumundadır.
    return true;
  };

  const isDeletable = (item: any) => {
    //kayıtlarda silme yapılacak ise hangi şartı sağlaması gerektiğinin seçimidir. şart için true dönmek o durumu editable yapar
    return true;
  };

  const renderColumn = (column: string, value: any) => {
    //bu ise column customize işlemi için
    if (typeof value === "boolean") {
      // mesela bool değerler için custom cell body
      return value ? <b>Yes</b> : <b>No</b>;
    }
    if (column == "age") {
      //veya tasarımsal customization
      return (
        value && (
          <i>
            <b>
              <u>{value}</u>
            </b>
          </i>
        )
      );
    }
    return value;
  };

  return (
    <>
      <Card title={"Employee List"}>
        <Board
          items={items}
          onEdit={handleEdit}
          onDelete={handleDelete}
          isEditable={isEditable}
          isDeletable={isDeletable}
          hiddenColumns={["id"]}
          renderColumn={renderColumn}
          hasNewRecordButton={true}
          newRecordButtonOnClick={() => { }}
        />
      </Card>
    </>
  );
};

export default Employee;
