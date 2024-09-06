import Button from "../CommonComponents/Button";
import Input from "../CommonComponents/Input";

function NumberInput(props: any) {
  return (
    <>
      <div key={props.key} className="form-group">
        <label htmlFor={props.key}>{props.label}</label>
        <div className="input-group">
          <div className="input-group-prepend mr-2">
            <Button
              onClick={() =>
                props.onChange((Number(props.value) - 1).toString())
              }
              type={props.type || "button"}
              className="btn btn-danger"
              text="-"
            />
          </div>
          <Input
            type="number"
            id={props.key}
            placeholder="Enter value"
            value={props.value}
            onChange={(e) => props.onChange(e.target.value)}
            required
            className="form-control"
          />
          <div className="input-group-append ml-2">
            <Button
              onClick={() =>
                props.onChange((Number(props.value) + 1).toString())
              }
              type={props.type || "button"}
              className="btn btn-success"
              text="+"
            />
          </div>
        </div>
      </div>
    </>
  );
}

export default NumberInput;
