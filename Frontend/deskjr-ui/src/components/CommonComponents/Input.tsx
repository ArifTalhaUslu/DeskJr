function Input(props: any) {
  return (
    <>
      <input
        type={props.type}
        className={props.className ?? "form-control"}
        id={props.id}
        value={props.value}
        onChange={(e) => {
          props.onChange(e);
        }}
        required={props.required}
        placeholder={props.placeholder}
        name={props.name}
      ></input>
    </>
  );
}
export default Input;
