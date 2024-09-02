import { useEffect, useState } from "react";
import Button from "../CommonComponents/Button";
import Input from "../CommonComponents/Input";

interface NumberInputProps {
  id: any;
  label: any;
  value: any;
  onValueChange: (id: any, newValue: any) => void;
  type?: any;
  className?: any;
  text?: any;
}

function NumberInput(props: NumberInputProps) {
  const [value, setValue] = useState<number>(props.value);

  const handleInputChange = (newValue: number) => {
    setValue(newValue);
    props.onValueChange(props.id, newValue.toString());
  };

  //   useEffect(() => {
  //     setValue(props.value);
  //   }, [props.value]);

  return (
    <>
      <div key={props.id} className="form-group">
        <label htmlFor={`${props.id}`}>{props.label}</label>
        <div className="input-group">
          <div className="input-group-prepend mr-2">
            <Button
              onClick={() => handleInputChange(Number(value) - 1)}
              type={props.type || "button"}
              className={props.className || "btn btn-danger"}
              text={props.text || "-"}
            />
          </div>
          <Input
            type="number"
            id={`${props.id}`}
            placeholder="Enter value"
            value={value}
            onChange={(e) => handleInputChange(Number(e.target.value) || 0)}
            required
            className="form-control"
          />
          <div className="input-group-append ml-2">
            <Button
              onClick={() => handleInputChange(Number(value) + 1)}
              type={props.type || "button"}
              className={props.className || "btn btn-success"}
              text={props.text || "+"}
            />
          </div>
        </div>
      </div>
    </>
  );
}

export default NumberInput;
