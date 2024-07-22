function Button(props: any) {
  return (
    <>
      <button
        onClick={props.onClick}
        type={props.type}
        className={
          props.className ? props.className : "btn btn-primary btn-block"
        }
      >
        {props.text ? props.text : 'Click'}
      </button>
    </>
  );
}
export default Button;
