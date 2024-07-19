import { useEffect, useState } from "react";
import DataTable from "../CommonComponents/DataTable";
import employeeService from "../../services/EmployeeService";

const Employee: any = () => {
    const [items, setItems] = useState([]);

    useEffect(() => {
      getList();
    });
  
    const getList = () => {
      employeeService.getAllEmployee().then((data) => {
        setItems(data);
      })
    };
  
    const handleEdit = (item: any) => {
      console.log("Edit item:", item);
      //kullanım örneği : 
      //service.updateItem(item);
    };
  
    const handleDelete = (item: any) => {
      console.log("Delete item:", item);
      //kullanım örneği : 
      //service.deleteItem(item.id);
    };
  
    const isEditable = (item: any) => {//kayıtlarda güncelleme yapılacak ise hangi şartı sağlaması gerektiğinin seçimidir. şart için true dönmek o durumu editable yapar
      //örneğin:
      //if (item.isBoss) return false; //bu durum için açıklama : isBoss özelliği true olan kayıtlar uneditable durumundadır.
      return true;
    };
  
    const isDeletable = (item: any) => {//kayıtlarda silme yapılacak ise hangi şartı sağlaması gerektiğinin seçimidir. şart için true dönmek o durumu editable yapar
      return true;
    };
  
    const renderColumn = (column: string, value: any) => {
      //bu ise column customize işlemi için
      if (typeof value === "boolean") {// mesela bool değerler için custom cell body
        return value ? <b>Yes</b> : <b>No</b>;
      }
      if (column == 'age') {//veya tasarımsal customization
        return value && <i><b><u>{value}</u></b></i>
      }
      return value;
    };




    return (
        <>
            <h1>baslik</h1>
            <DataTable items={items} />
        </>
    );
};

export default Employee;
