function Input(props:any){
    return(
    <>
    <input 
    type = {props.type}
    className = {props.className}
    id = {props.id}
    value = {props.value}
    onChange = {(e)=>{props.onChange(e.target.value)}}
    required = {props.required}
    placeholder = {props.placeholder}
    ></input>
    </>
    )
}
export default Input;