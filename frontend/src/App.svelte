<script lang="ts">
	import {onMount} from "svelte";

	export let apiUrl: string;

	let time: Date | null = null;
	let error: Error | null = null;

	$: formattedTime = time != null ? `${time!.getHours()}:${time.getMinutes()}:${time.getSeconds()}` : null;

	onMount(() => {
		let interval;
		getCurrentTimeFromApi()
			.then(date => {
				time = date;
				interval = setInterval(() => {
					time = new Date(time.getTime() + 1000);
				}, 1000);
			}).catch(err => {
				error = err;
			})
		return () => clearInterval(interval);
	})

	async function getCurrentTimeFromApi(): Promise<Date> {
		const res = await fetch(apiUrl);

		if (res.ok) {
			const dto = await res.json();
			const x = dto.time.split(':');
			return new Date(0,0,0,parseInt(x[0]),parseInt(x[1]),parseInt(x[2]));
		}else {
			throw Error(`Invalid Response.`);
		}
	}
</script>

<main>
	{#if error}
		<p>Error: {error.message}</p>
	{:else if !time}
		<p>Fetching...</p>
	{:else }
		<p>Current Time: {formattedTime}</p>
	{/if}
</main>
