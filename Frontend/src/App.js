import React, { useContext } from 'react';
import {
	BrowserRouter as Router,
	Switch,
	Route,
} from "react-router-dom";
import Header from "./components/Header";
import PrivateRoute from "./components/PrivateRoute";
import Livros from "./pages/livros";
import DetalheLivro from "./pages/livros/Detalhe";
import Login from "./pages/usuarios/Login";
import { store } from './store.js';

function App() {
	const globalState = useContext(store);

	return (
		<Router>
			<Header />
			<div className="mt-4">
				<Switch>
					<Route exact path="/">
						{globalState.usuario && <Livros />}
						{!globalState.usuario && <Login />}
					</Route>
					<PrivateRoute exact path="/livros" component={Livros} />
					<Route path="/livros/:slug" component={DetalheLivro} />
				</Switch>
			</div>
		</Router>
	);
}

export default App;
