enum gState {
	setupscene,
	intro,
	getready,
	choose,
	showresult,
	killloser,
	movetonextscene,
	victory,
	finalvictory,
};

public enum choice {
	undecided,
	rock,
	scissors,
	paper
};

public enum winner {
	p1wins,
	p2wins,
	tie
}

public enum pState {	// player states
	introrun,
	idle,
	win,
	lose,
	winrun
};