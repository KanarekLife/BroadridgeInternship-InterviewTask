import App from "./App.svelte";

const app = new App({
	target: document.body,
	props: {
		apiUrl: "http://localhost:5000/time/",
	},
});

export default app;
