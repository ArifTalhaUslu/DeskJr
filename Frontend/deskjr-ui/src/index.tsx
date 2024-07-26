import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import { Provider } from "react-redux";
import { applyMiddleware } from "redux";
import { createStore } from "redux";
import thunk from "redux-thunk";
import rootReducer from "./store";

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement
);

const store = createStore(rootReducer, applyMiddleware(thunk));

root.render(
    <Provider store={store}>
        <App />
    </Provider>
);